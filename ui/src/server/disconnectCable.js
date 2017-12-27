import save from '../utils/save';

export default async function disconnectCable(jackUuid, cableUuid) {
  await fetch(`http://localhost:8080/jack/${jackUuid}/disconnectCable/${cableUuid}`, {
    method: 'POST',
  });
  await save();
}
