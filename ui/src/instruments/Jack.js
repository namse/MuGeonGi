import React, { Component } from 'react';
import Canvas from '../canvas/Canvas';

const jackMap = {}; // uuid, jack
const jackFindingJobs = {}; // uuid, handler

export function findJack(uuid) {
  return new Promise((resolve, reject) => {
    const foundJack = jackMap[uuid];
    if (foundJack) {
      resolve(foundJack);
    } else {
      jackFindingJobs[uuid] = jack => resolve(jack);
    }
  });
}

export default class Jack extends Component {
  componentWillMount() {
    const { uuid } = this.props;
    jackMap[uuid] = this;
    const handler = jackFindingJobs[uuid];
    if (handler) {
      handler(this);
    }
  }
  componentWillUnmount() {
    const { uuid } = this.props;
    jackMap[uuid] = undefined;
    if (this.cable) {
      this.cable.destroy();
    }
  }
  async onMouseDown() {
    if (!this.cable) {
      Canvas.onJackClicked(this);
      return;
    }
    console.log('i am already connected with cable!');
    const { cable } = this;
    await this.cable.detachJack(this);
    Canvas.AttachCableToMouse(cable);
  }
  onMouseUp() {
    if (this.cable) {
      console.log('i am already connected with cable!');
      return;
    }
    console.log('i am jack on mouse up function');
    Canvas.onMouseUpOnJack(this);
  }
  setCable(cable) {
    this.cable = cable;
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
  render() {
    return (
      <div
        ref={(startPoint) => { this.startPoint = startPoint; }}
        style={{
          'user-select': 'none',
          width: '20px',
          height: '20px',
          position: 'relative',
          top: '40px',
        }}
        onMouseDown={() => this.onMouseDown()}
        onMouseUp={() => this.onMouseUp()}
      >
        O
      </div>
    );
  }
}
