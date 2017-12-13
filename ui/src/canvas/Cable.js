import Sprite from './Sprite';
import getMousePosition from '../utils/getMousePosition';

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

export default class Cable extends Sprite {
  constructor(startJackDOM) {
    super();
    this.startJackDOM = startJackDOM;
  }
  render(ctx) {
    const {
      x: startX,
      y: startY,
    } = GetPosition(this.startJackDOM);
    const {
      x: endX,
      y: endY,
    } = (this.endJackDOM) ? GetPosition(this.endJackDOM) : getMousePosition();
    ctx.moveTo(startX, startY);
    ctx.lineTo(endX, endY);
    ctx.stroke();
  }
}
