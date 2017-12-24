import instrumentList, { addEventListener } from './instrumentList';

const fs = window.require('fs');

export default function save() {
  return new Promise((resolve, reject) => {
    const instrumentData = instrumentList.map(instrument => ({
      name: instrument.type.name,
      props: instrument.props,
    }));
    const string = JSON.stringify(instrumentData, null, 2);
    console.log(string);
    fs.writeFile('.save', string, (err) => {
      if (err) {
        return reject(err);
      }
      return resolve();
    });
  });
}

addEventListener(() => {
  save();
});
