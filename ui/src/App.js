import React, { Component } from 'react';
import createInstrument from './server/createInstrument';
import Mic from './Mic';
import Speaker from './Speaker';
import HighpassFilter from './HighpassFilter';
import Canvas from './canvas/Canvas';

class App extends Component {
  constructor(props) {
    super(props);

    this.state = {
      instruments: [],
    };
    createInstrument('Mic')
      .then((props) => {
        console.log(props);
        this.setState({
          instruments: this.state.instruments.concat(<Mic {...props} />),
        });
      });
    createInstrument('Speaker')
      .then((props) => {
        console.log(props);
        this.setState({
          instruments: this.state.instruments.concat(<Speaker {...props} />),
        });
      });
    createInstrument('HighpassFilter')
      .then((props) => {
        console.log(props);
        this.setState({
          instruments: this.state.instruments.concat(<HighpassFilter {...props} />),
        });
      });
  }
  render() {
    return (
      <div
        className="App"
        onMouseUp={() => Canvas.onMouseUp()}
      >
        {this.state.instruments}
      </div >
    );
  }
}

export default App;
