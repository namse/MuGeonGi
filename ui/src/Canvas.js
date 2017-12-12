const canvas = document.getElementById('canvas');
canvas.width = window.innerWidth;
canvas.height = window.innerHeight;

const ctx = canvas.getContext('2d');

const canvasState = {
  IDLE: 'IDLE',
  CABLING: 'CABLING',
};

let state = canvasState.IDLE;

const pairs = [];

let currentJackDOM;
let mouseX = 0;
let mouseY = 0;

document.addEventListener('mousemove', (e) => {
  mouseX = e.pageX;
  mouseY = e.pageY;
});

function onMouseUp() {
  if (state === canvasState.CABLING) {
    state = canvasState.IDLE;
    currentJackDOM = undefined;
  }
}
function GetPosition(dom) {
  const {
    left,
    top,
    right,
    bottom,
  } = dom.getBoundingClientRect();
  const x = (left + right) / 2;
  const y = (top + bottom) / 2;
  return { x, y };
}

function drawCabling() {
  if (currentJackDOM) {
    const { x, y } = GetPosition(currentJackDOM);
    ctx.moveTo(x, y);
    ctx.lineTo(mouseX, mouseY);
    ctx.stroke();
  }
}

function drawCables() {
  pairs.forEach(({ start, end }) => {
    const {
      x: startX,
      y: startY,
    } = GetPosition(start);
    const {
      x: endX,
      y: endY,
    } = GetPosition(end);
    ctx.moveTo(startX, startY);
    ctx.lineTo(endX, endY);
    ctx.stroke();
  });
}

function draw() {
  canvas.width = canvas.width;

  // DRAW SOMETHING HERE!!!!
  drawCabling();
  drawCables();

  setTimeout(draw, 0);
}
draw();

export default {
  onJackClicked(jackDOM) {
    if (state === canvasState.IDLE) {
      state = canvasState.CABLING;
      currentJackDOM = jackDOM;
    }
  },
  onMouseUpOnJack(jackDOM) {
    if (state === canvasState.CABLING) {
      if (currentJackDOM !== jackDOM) {
        const pair = {
          start: currentJackDOM,
          end: jackDOM,
        };
        pairs.push(pair);
      }
      state = canvasState.IDLE;
      currentJackDOM = undefined;
    }
  },
  onMouseUp,
};
