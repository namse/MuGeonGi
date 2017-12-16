import React, { Component } from 'react';
import styled from 'styled-components';
import createInstrument from './server/createInstrument';
import Mic from './instruments/Mic';
import Speaker from './instruments/Speaker';
import HighpassFilter from './instruments/HighpassFilter';
import AudioPlayer from './instruments/AudioPlayer';

const instrumentClassMap = {
  Mic,
  Speaker,
  HighpassFilter,
  AudioPlayer,
};
const instrumentList = Object.keys(instrumentClassMap);

const Conatiner = styled.div`
  height: 100%;
  width: 100px;
  position: fixed;
  z-index: 1;
  top: 0;
  left: 0;
  overflow-x: hidden;
`;

export default class Menu extends Component {
  onClick(instrument) {
    createInstrument(instrument)
      .then((props) => {
        const instrumentClass = instrumentClassMap[instrument];
        console.log(instrumentClass);
        const newInstrumentComponent = React.createElement(instrumentClass, props, null);
        this.props.onNewInstrument(newInstrumentComponent);
      });
  }
  render() {
    return (
      <Conatiner>
        {instrumentList.map(instrument => (
          <div>
            <button onClick={() => this.onClick(instrument)}>{instrument}</button>
          </div>
        ))}
      </Conatiner >
    );
  }
};
