import React, { Component } from 'react';
import createInstrument from './createInstrument';
import Mic from './Mic';
import Speaker from './Speaker';
import Canvas from './canvas/Canvas';

class App extends Component {
  constructor(props) {
    super(props);

    this.state = {
      instruments: [],
    };
    createInstrument('mic')
      .then(uuid => this.setState({
        instruments: this.state.instruments.concat(<Mic uuid={uuid} />),
      }));

    createInstrument('speaker')
      .then(uuid => this.setState({
        instruments: this.state.instruments.concat(<Speaker uuid={uuid} />),
      }));
  }
  render() {
    return (
      <div
        className="App"
        onMouseUp={() => Canvas.onMouseUp()}
      >
        {this.state.instruments}
      </div>
    );
  }
}

export default App;
