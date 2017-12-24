import instrumentList, { onInstrumentAdded } from './instrumentList';
import cableList, { onCableAdded } from './cableList';

const fs = window.require('fs');

export default function save() {
  return new Promise((resolve, reject) => {
    const instrumentData = instrumentList.map(instrument => ({
      name: instrument.type.name,
      props: instrument.props,
    }));
    console.log(cableList);
    const cableData = cableList.map((cable) => {
      const startJack = cable.startJack
        ? { uuid: cable.startJack.props.uuid }
        : undefined;
      const endJack = cable.endJack
        ? { uuid: cable.endJack.props.uuid }
        : undefined;
      return {
        uuid: cable.uuid,
        startJack,
        endJack,
      };
    });
    console.log(cableData);
    const string = JSON.stringify({
      instruments: instrumentData,
      cables: cableData,
    }, null, 2);

    fs.writeFile('.save', string, (err) => {
      if (err) {
        return reject(err);
      }
      return resolve();
    });
  });
}

onInstrumentAdded(() => save());
onCableAdded(() => save());
