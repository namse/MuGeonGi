import React, { Component } from 'react';

export default class Cable extends Component {
  static defaultProps = {
    jack1: undefined,
    jack2: undefined,
    mousePosition: undefined,
  }

  redner() {
    return (
      <div
        style={{
          position: 'absolute',
          left: `${x}px`,
          top: `${y}px`,
          height: `${length}px`,
          transform: `rotate(${angle}deg)`,
          width: '3px',
          backgroundColor: '#007FFF',
          transformOrigin: 'top left',
        }}
      />
    );
  }
}
