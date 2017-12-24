import save from '../utils/save';

export default function connectCable(jackUuid, cableUuid) {
  fetch(`http://localhost:8080/jack/${jackUuid}/connectCable/${cableUuid}`, {
    method: 'POST',
  })
    .then(() => {
      save();
    });
}
