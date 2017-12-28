import Sprite from './Sprite';
import getMousePosition from '../utils/getMousePosition';
import connectCable from '../server/connectCable';
import disconnectCable from '../server/disconnectCable';
import destroyInstrument from '../server/destroyInstrument';

export const cableList = [];

export default class Cable extends Sprite {
  constructor({ uuid }) {
    super();
    this.uuid = uuid;
    this.isDestroying = false;
    cableList.push(this);
  }
  async destroy() {
    if (this.isDestroying) {
      return;
    }
    this.isDestroying = true;
    const index = cableList.findIndex(cable => cable === this);
    cableList.splice(index, 1);
    if (this.endJack) {
      await this.detachJack(this.endJack);
    }
    if (this.startJack) {
      await this.detachJack(this.startJack);
    }
    await destroyInstrument(this.uuid);
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
  // This function will not destory cable
  async detachJack(jack) {
    jack.setCable(null);

    if (jack === this.startJack) {
      this.startJack = this.endJack;
    }
    this.endJack = null;

    await disconnectCable(jack.props.uuid, this.uuid);
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
