import Sprite from './Sprite';
import getMousePosition from '../utils/getMousePosition';
import connectCable from '../server/connectCable';
import disconnectCable from '../server/disconnectCable';

export const cableList = [];

export default class Cable extends Sprite {
  constructor({ uuid }) {
    super();
    this.uuid = uuid;
    cableList.push(this);
  }
  destroy() {
    const index = cableList.findIndex(cable => cable === this);
    cableList.splice(index, 1);
    if (this.startJack) {
      this.startJack.setCable(null);
    }
    if (this.endJack) {
      this.endJack.setCable(null);
    }
  }
  setStartJack(startJack) {
    this.startJack = startJack;
    startJack.setCable(this);
    return connectCable(startJack.props.uuid, this.uuid);
  }
  setEndJack(endJack) {
    this.endJack = endJack;
    endJack.setCable(this);
    return connectCable(endJack.props.uuid, this.uuid);
  }
  detachJack(jack) {
    if (jack === this.startJack) {
      this.startJack = this.endJack;
    }
    this.endJack = null;
    return disconnectCable(jack.props.uuid, this.uuid);
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
