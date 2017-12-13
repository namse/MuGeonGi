import Cable from './Cable';

const canvas = document.getElementById('canvas');
canvas.width = window.innerWidth;
canvas.height = window.innerHeight;

const ctx = canvas.getContext('2d');

const canvasState = {
  IDLE: 'IDLE',
  CABLING: 'CABLING',
};

let state = canvasState.IDLE;

let connectingCable;

function onMouseUp() {
  if (state === canvasState.CABLING) {
    state = canvasState.IDLE;
    connectingCable = undefined;
  }
}

const sprites = [];
const cables = [];

function draw() {
  canvas.width = canvas.width;

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
