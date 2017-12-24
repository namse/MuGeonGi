const list = [];
const eventHandlers = [];

export default list;

export function addCable(cable) {
  list.push(cable);
  eventHandlers.forEach(handler => handler(list));
}

export function removeCable(cable) {
  // TODO
}

export function onCableAdded(eventHandler) {
  eventHandlers.push(eventHandler);
}
