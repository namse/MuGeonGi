import Sprite from './Sprite';
import getMousePosition from '../utils/getMousePosition';
import createInstrument from '../server/createInstrument';


export default class Cable extends Sprite {
  constructor(startJack) {
    super();
    createInstrument('cable')
      .then((uuid) => {
        this.uuid = uuid;
        this.startJack.connectCable(uuid);
      });
    this.startJack = startJack;
  }
  render(ctx) {
    const {
      x: startX,
      y: startY,
    } = this.startJack.getPosition();
    const {
      x: endX,
      y: endY,
    } = (this.endJack) ? this.endJack.getPosition() : getMousePosition();
    ctx.moveTo(startX, startY);
    ctx.lineTo(endX, endY);
    ctx.stroke();
  }
}
