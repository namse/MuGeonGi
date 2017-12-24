import React from 'react';
import { addInstrument } from '../utils/instrumentList';
import instrumentClassMap from '../utils/instrumentClassMap';

export default instrument =>
  fetch(`http://localhost:8080/${instrument}`, {
    method: 'POST',
  })
    .then(res => res.json())
    .then((props) => {
      const instrumentClass = instrumentClassMap[instrument];
      const newInstrumentComponent = React.createElement(instrumentClass, props, null);
      addInstrument(newInstrumentComponent);
    });
