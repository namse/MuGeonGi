import React, { Component } from 'react';
import Canvas from './Canvas';

export default class Jack extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isDrawingCable: false,
    };
  }
  onMouseDown = () => {
    Canvas.onJackClicked(this.startPoint);
  }
  onMouseUp = () => {
    Canvas.onMouseUpOnJack(this.startPoint);
  }
  render() {
    return (
      <div
        ref={(startPoint) => { this.startPoint = startPoint; }}
        style={{
          'user-select': 'none',
        }}
        onMouseDown={() => this.onMouseDown()}
        onMouseUp={() => this.onMouseUp()}
      >
        MyNameIsJackBlack!
      </div>
    );
  }
}
