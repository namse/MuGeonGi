const fs = window.require('fs');
const path = window.require('path');

console.log(window.__dirname);
fs.readdir(path.resolve(__dirname, '../instrument'), (err, files) => {
  if (err) {
    throw new Error(err);
  }
  console.log(files);
});

// const instrumentClassMap = {
//   Mic,
//   Speaker,
//   HighpassFilter,
//   AudioPlayer,
// };
// const instrumentList = Object.keys(instrumentClassMap);


// export default instrument =>
//   fetch(`http://localhost:8080/${instrument}`, {
//     method: 'POST',
//   })
//     .then(res => res.json())
//     .then((props) => {

//       createInstrument(instrument)
//       .then((props) => {
//         const instrumentClass = instrumentClassMap[instrument];
//         console.log(instrumentClass);
//         const newInstrumentComponent = React.createElement(instrumentClass, props, null);
//         this.props.onNewInstrument(newInstrumentComponent);
//       });
//     });
