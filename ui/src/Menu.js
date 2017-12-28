import React, { Component } from 'react';
import styled from 'styled-components';
import createInstrument from './server/createInstrument';
import instrumentClassMap from './utils/instrumentClassMap';
import { deleteAllInstruments } from './App';

const Conatiner = styled.div`
  height: 70%;
  width: 100px;
  position: fixed;
  z-index: 1;
  top: 0;
  left: 0;
  overflow-x: hidden;
  border: 1px solid blue;
`;

const ResetButton = styled.button`
  bottom: 0;
  position: absolute;
`;

export default class Menu extends Component {
  constructor(props) {
    super(props);

    const instrumentNames = Object.keys(instrumentClassMap);
    this.state = {
      instrumentNames,
    };
  }
  onClick(instrument) {
    createInstrument(instrument);
  }
  render() {
    return (
      <Conatiner>
        {this.state.instrumentNames.map(instrument => (
          <div>
            <button onClick={() => this.onClick(instrument)}>{instrument}</button>
          </div>
        ))}
        <ResetButton
          onClick={() => deleteAllInstruments()}
        >Reset
        </ResetButton>
      </Conatiner >
    );
  }
}
