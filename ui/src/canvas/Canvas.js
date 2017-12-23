import Cable from './Cable';
import destroyInstrument from '../server/destroyInstrument';

const canvas = document.getElementById('canvas');
const ctx = canvas.getContext('2d');

const canvasState = {
  IDLE: 'IDLE',
  CABLING: 'CABLING',
};

let state = canvasState.IDLE;

let connectingCable;
let sprites = [];
let cables = [];


function onMouseUp() {
  if (state === canvasState.CABLING) {
    state = canvasState.IDLE;
    destroyInstrument(connectingCable.uuid)
      .then(() => {
        cables = cables.filter(cable => cable !== connectingCable);
        sprites = sprites.filter(sprite => sprite !== connectingCable);
        connectingCable = undefined;
      });
  }
}

function draw() {
  canvas.width = window.innerWidth;
  canvas.height = window.innerHeight;

  sprites.forEach(cable => cable.render(ctx));

  setTimeout(draw, 0);
}
draw();

export default {
  onJackClicked(jack) {
    if (state === canvasState.IDLE) {
      state = canvasState.CABLING;
      connectingCable = new Cable(jack);
      sprites.push(connectingCable);
    }
  },
  onMouseUpOnJack(jack) {
    if (state === canvasState.CABLING) {
      if (connectingCable.startJack !== jack) {
        connectingCable.setEndJack(jack);
        cables.push(connectingCable);
      }
      state = canvasState.IDLE;
      connectingCable = undefined;
    }
  },
  onMouseUp,
};
