import React, { Component } from 'react';

let drawingJack;
function onMouseDown(jack) {
  drawingJack = jack;
  console.log('down');
}
function onMouseUp(jack) {
  console.log('up');
  drawingJack = undefined;
}
function onMouseMove(event) {
  console.log('move');
  if (drawingJack) {
    drawingJack.onMouseMove(event);
  }
}
document.addEventListener('mousemove', onMouseMove);

export default class Jack extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isDrawingCable: false,
    };
  }
  onMouseMove(event) {
    const {
      pageX: mouseX,
      pageY: mouseY,
    } = event;
    const {
      left: x,
      top: y,
    } = this.startPoint.getBoundingClientRect();
    const length = (((mouseX - x) ** 2) + ((mouseY - y) ** 2)) ** 0.5;
    const angle = -1 * (180 / Math.PI) * Math.acos((mouseY - y) / length);
    this.setState({
      length,
      angle,
      x,
      y,
    });
  }
  render() {
    return (
      <div
        ref={(startPoint) => { this.startPoint = startPoint; }}
        style={{
          'user-select': 'none',
        }}
        onMouseDown={() => onMouseDown(this)}
        onMouseUp={() => onMouseUp(this)}
      >
        MyNameIsJackBlack!
        <Cable {...this.state} />
      </div>
    );
  }
}
