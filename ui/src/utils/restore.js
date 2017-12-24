import createInstrument from '../server/createInstrument';
import createCable from '../server/createCable';
import { findJack } from '../instruments/Jack';

const fs = window.require('fs');

async function restoreInstruments(instruments) {
  const createdInstruments = {};
  const promises = instruments.map(instrument =>
    createInstrument(instrument.name)
      .then((newInstrument) => {
        createdInstruments[instrument.props.uuid] = newInstrument;
      }));
  return Promise.all(promises).then(() => createdInstruments);
}

async function restoreCables({
  cables,
  oldInstruments,
  newInstruments,
}) {
  await Promise.all(cables.map(async (cable) => {
    console.log(cable);
    if (!cable.startJack || !cable.endJack) {
      return;
    }
    let newStartJack;
    let newEndJack;
    console.log(oldInstruments);
    await Promise.all(oldInstruments.map(async (instrument) => {
      const jackNames = ['inputJack', 'outputJack'];
      await Promise.all(jackNames.map(async (jackName) => {
        if (instrument.props[jackName]) {
          const newInstrument = newInstruments[instrument.props.uuid];
          const jackUuid = instrument.props[jackName].uuid;
          const newJack = await findJack(newInstrument.props[jackName].uuid);
          if (jackUuid === cable.startJack.uuid) {
            newStartJack = newJack;
          } else if (jackUuid === cable.endJack.uuid) {
            newEndJack = newJack;
          }
        }
      }));
    }));
    const newCable = await createCable();
    await newCable.setStartJack(newStartJack);
    await newCable.setEndJack(newEndJack);
    console.log(newCable);
  }));
}

export default function restore() {
  return new Promise((resolve, reject) => {
    fs.readFile('.save', 'utf8', async (err, data) => {
      if (err) {
        return reject(err);
      }
      try {
        const {
          instruments,
          cables,
        } = JSON.parse(data);
        console.log(instruments);
        const newInstruments = await restoreInstruments(instruments);
        console.log(cables);
        restoreCables({
          cables,
          oldInstruments: instruments,
          newInstruments,
        });
        return resolve();
      } catch (err2) {
        return reject(err2);
      }
    });
  });
}
