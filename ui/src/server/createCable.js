import Cable from '../canvas/Cable';
import save from '../utils/save';

export default () =>
  fetch('http://localhost:8080/Cable', {
    method: 'POST',
  })
    .then(res => res.json())
    .then((props) => {
      const cable = new Cable(props);
      save();
      return cable;
    });
