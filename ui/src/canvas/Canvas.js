import { cableList } from './Cable';
import destroyInstrument from '../server/destroyInstrument';
import createCable from '../server/createCable';

const canvas = document.getElementById('canvas');
const ctx = canvas.getContext('2d');

const canvasState = {
  IDLE: 'IDLE',
  CABLING: 'CABLING',
};

let state = canvasState.IDLE;

let connectingCable;

async function onMouseUp() {
  if (state === canvasState.CABLING) {
    state = canvasState.IDLE;
    await destroyInstrument(connectingCable.uuid);
    connectingCable.destroy();
    connectingCable = undefined;
  }
}

function draw() {
  canvas.width = window.innerWidth;
  canvas.height = window.innerHeight;

  cableList.forEach(cable => cable.render(ctx));

  setTimeout(draw, 0);
}
draw();

export default {
  onJackClicked(jack) {
    if (state === canvasState.IDLE) {
      state = canvasState.CABLING;
      createCable()
        .then((cable) => {
          connectingCable = cable;
          cable.setStartJack(jack);
        });
    }
  },
  onMouseUpOnJack(jack) {
    if (state === canvasState.CABLING) {
      if (connectingCable.startJack !== jack) {
        connectingCable.setEndJack(jack);
      }
      state = canvasState.IDLE;
      connectingCable = undefined;
    }
  },
  onMouseUp,
  AttachCableToMouse(cable) {
    connectingCable = cable;
    state = canvasState.CABLING;
  },
};
