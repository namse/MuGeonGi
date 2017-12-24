import React, { Component } from 'react';
import styled from 'styled-components';
import Canvas from './canvas/Canvas';
import Menu from './Menu';
import { onInstrumentAdded } from './utils/instrumentList';
import restore from './utils/restore';

const Container = styled.div`
  overflow: hidden;
`;
const PlayGround = styled.div`
  width: calc(100vw - 100px);
  height: 100vh;
  border: 1px solid red;
  left: 100px;
  position: fixed; /* Stay in place */  
`;

class App extends Component {
  constructor(props) {
    super(props);

    this.state = {
      instruments: [],
    };
    onInstrumentAdded((instruments) => {
      console.log(instruments);
      this.setState({
        instruments,
      });
    });
    restore()
      .catch(err => console.log(err));
  }
  render() {
    return (
      <Container>
        <Menu />
        <PlayGround
          className="App"
          onMouseUp={() => Canvas.onMouseUp()}
        >
          {this.state.instruments}
        </PlayGround >
      </Container>
    );
  }
}

export default App;
