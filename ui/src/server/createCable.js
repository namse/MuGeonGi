import React from 'react';
import Cable from '../canvas/Cable';
import { addCable } from '../utils/cableList';

export default () =>
  fetch('http://localhost:8080/Cable', {
    method: 'POST',
  })
    .then(res => res.json())
    .then((props) => {
      const cable = new Cable(props);
      addCable(cable);
      return cable;
    });
