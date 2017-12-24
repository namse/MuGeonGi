import React, { Component } from 'react';
import SingleBox from './SingleBox';
import Jack from './Jack';

export default class Speaker extends Component {
  constructor(props) {
    super(props);
    this.state = {
      devices: [],
    };
    const {
      uuid,
    } = props;
    fetch(`http://localhost:8080/speaker/${uuid}/devices`)
      .then(res => res.json())
      .then(devices => this.setState({ devices: ['', ...devices] }));
  }
  componentWillUnmount() {
    const { uuid } = this.props;
    fetch(`http://localhost:8080/instrument/${uuid}`, {
      method: 'delete',
    })
      .then(res => console.log(`delete speaker : ${res.status}`));
  }
  setDevice = (device) => {
    console.log(device);
    if (device.length <= 0) {
      return;
    }
    const { uuid } = this.props;
    fetch(`http://localhost:8080/speaker/${uuid}/device/${device}`, {
      method: 'post',
    })
      .then(res => console.log(`set device of speaker : ${res.status}`));
  }
  turnOn = () => {
    const { uuid } = this.props;
    fetch(`http://localhost:8080/speaker/${uuid}/TurnOn`, {
      method: 'post',
    })
      .then(res => console.log(`turn on speaker : ${res.status}`));
  }
  render() {
    const {
      devices,
      selectedDevice,
    } = this.state;
    const options = devices.map(device => <option value={device}>{device}</option>);
    return (
      <SingleBox {...this.props}>
        Speaker
        Device:
        <select
          style={{ width: '100%' }}
          onChange={event => this.setDevice(event.target.value)}
          value={selectedDevice}
        >
          {options}
        </select>
        <button onClick={() => this.turnOn()}>Turn On</button>
      </SingleBox>
    );
  }
}
