import save from '../utils/save';

export default uuid =>
  fetch(`http://localhost:8080/instrument/${uuid}`, {
    method: 'DELETE',
  })
    .then((res) => {
      if (res.status < 200 && res.status >= 300) {
        throw new Error(res.statusText);
      }
      save();
    });
