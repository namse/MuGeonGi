import React, { Component } from 'react';
import Canvas from './canvas/Canvas';

export default class Jack extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isDrawingCable: false,
    };
  }
  getPosition() {
    const {
      left,
      top,
      right,
      bottom,
    } = this.startPoint.getBoundingClientRect();
    const x = (left + right) / 2;
    const y = (top + bottom) / 2;
    return { x, y };
  }
  connectCable(cableUuid) {
    const {
      uuid,
    } = this.props;
    fetch(`http://localhost:8080/jack/${uuid}/connectCable/${cableUuid}`, {
      method: 'POST',
    })
      .then(res => {
        console.log(res);
      })
  }
  onMouseDown = () => {
    Canvas.onJackClicked(this);
  }
  onMouseUp = () => {
    Canvas.onMouseUpOnJack(this);
  }
  render() {
    return (
      <div
        ref={(startPoint) => { this.startPoint = startPoint; }}
        style={{
          'user-select': 'none',
          width: '20px',
          height: '20px',
          position: 'relative',
          top: '40px'
        }}
        onMouseDown={() => this.onMouseDown()}
        onMouseUp={() => this.onMouseUp()}
      >
        O
      </div>
    );
  }
}
