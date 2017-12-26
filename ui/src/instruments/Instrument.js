import React, { Component } from 'react';
import save from '../utils/save';

export const instrumentList = [];
export const stateMap = {}; // uuid, state

const onInstrumentAddedEventHandlers = [];

export function onInstrumentAdded(eventHandler) {
  onInstrumentAddedEventHandlers.push(eventHandler);
}

const instrumentMap = {}; // uuid, Instrument
const instrumentFindingJobs = {}; // uuid, handler

export function findInstrument(uuid) {
  return new Promise((resolve, reject) => {
    const foundInstrument = instrumentMap[uuid];
    console.log(foundInstrument);
    if (foundInstrument) {
      resolve(foundInstrument);
    } else {
      instrumentFindingJobs[uuid] = instrument => resolve(instrument);
    }
  });
}

export default class Instrument extends Component {
  componentWillMount() {
    this.saveState();
    const { uuid } = this.props;
    instrumentList.push(this);
    instrumentMap[uuid] = this;
    const handler = instrumentFindingJobs[uuid];
    if (handler) {
      handler(this);
    }
    onInstrumentAddedEventHandlers.forEach(addedEventHandler => addedEventHandler(this));
    this.initializeFunctions();
  }
  componentDidUpdate() {
    this.saveState();
  }
  componentWillUnmount() {
    const { uuid } = this.props;
    fetch(`http://localhost:8080/instrument/${uuid}`, {
      method: 'delete',
    })
      .then(res => console.log(`delete instrument : ${res.status}`));
    // TODO : List에서 지우세요 좋은 말 할 때
  }
  saveState() {
    const {
      StatesWillSave,
    } = this.constructor;
    if (!StatesWillSave) {
      return;
    }
    const state = {};
    StatesWillSave.forEach((stateName) => {
      state[stateName] = this.state[stateName];
    });
    stateMap[this.props.uuid] = state;
  }
  initializeFunctions() {
    console.log(this.constructor.StatesWillSave);
    if (!this.constructor.StatesWillSave) {
      return;
    }
    this.constructor.StatesWillSave.forEach((stateName) => {
      const methodName = `set${stateName.slice(0, 1).toUpperCase()}${stateName.slice(1)}`;
      this[methodName] = (state) => {
        this.setState({
          [stateName]: state,
        });
        const { uuid } = this.props;
        // TODO:
        return fetch(`http://localhost:8080/${this.constructor.name}/${uuid}/${stateName}/${state}`, {
          method: 'post',
        })
          .then((res) => {
            console.log(`${this.constructor.name}/${uuid}/${stateName}/${state} : ${res.status}`);
            return save();
          });
      };
    });
  }
}
