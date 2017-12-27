import createInstrument from '../server/createInstrument';
import createCable from '../server/createCable';
import { findJack } from '../instruments/Jack';
import { findSingleBox } from '../instruments/SingleBox';

const fs = window.require('fs');

async function restoreInstruments(instruments) {
  const newInstrumentMap = {};
  await Promise.all(instruments.map(async (instrument) => {
    const newInstrument = await createInstrument(instrument.name);

    const singleBox = await findSingleBox(newInstrument.props.uuid);
    const { x, y } = instrument;
    singleBox.setDefaultPosition(x, y);

    if (!instrument.state) {
      return;
    }
    await Promise.all(Object.keys(instrument.state).map(async (key) => {
      const method = `set${key.slice(0, 1).toUpperCase()}${key.slice(1)}`;
      const param = instrument.state[key];
      await newInstrument[method](param);
    }));
    newInstrumentMap[instrument.props.uuid] = newInstrument;
  }));
  return newInstrumentMap;
}

async function restoreCables({
  cableList,
  oldInstruments,
  newInstrumentMap, // old UUID, new Instrument Instance
}) {
  await Promise.all(cableList.map(async (cable) => {
    if (!cable.startJack || !cable.endJack) {
      return;
    }
    let newStartJack;
    let newEndJack;
    await Promise.all(oldInstruments.map(async (instrument) => {
      const jackNames = ['inputJack', 'outputJack'];
      await Promise.all(jackNames.map(async (jackName) => {
        if (instrument.props[jackName]) {
          const newInstrument = newInstrumentMap[instrument.props.uuid];
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
          cableList,
        } = JSON.parse(data);

        const newInstrumentMap = await restoreInstruments(instruments);

        restoreCables({
          cableList,
          oldInstruments: instruments,
          newInstrumentMap,
        });
        return resolve();
      } catch (err2) {
        return reject(err2);
      }
    });
  });
}
