import Sprite from './Sprite';
import getMousePosition from '../utils/getMousePosition';
import connectCable from '../server/connectCable';

export default class Cable extends Sprite {
  constructor({ uuid }) {
    super();
    this.uuid = uuid;
  }
  setStartJack(startJack) {
    this.startJack = startJack;
    return connectCable(startJack.props.uuid, this.uuid);
  }
  setEndJack(endJack) {
    this.endJack = endJack;
    return connectCable(endJack.props.uuid, this.uuid);
  }
  render(ctx) {
    const {
      x: startX,
      y: startY,
    } = this.startJack.getPosition();
    const {
      x: endX,
      y: endY,
    } = this.endJack ? this.endJack.getPosition() : getMousePosition();
    ctx.moveTo(startX, startY);
    ctx.lineTo(endX, endY);
    ctx.stroke();
  }
}
