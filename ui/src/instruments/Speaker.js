import React from 'react';
import SingleBox from './SingleBox';
import Instrument from './Instrument';
import SettingPortal from './SettingPortal';

export default class Speaker extends Instrument {
  static StatesWillSave = [
    'device',
    'volume',
  ];
  constructor(props) {
    super(props);
    this.state = {
      devices: [],
      selectedDevice: false,
      volume: 1,
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
  turnOn = () => {
    const { uuid } = this.props;
    fetch(`http://localhost:8080/speaker/${uuid}/TurnOn`, {
      method: 'post',
    })
      .then(res => console.log(`turn on speaker : ${res.status}`));
  }
  renderSetting() {
    const {
      devices,
      selectedDevice,
      volume,
    } = this.state;
    const options = devices.map(device => <option value={device}>{device}</option>);
    return (
      <SettingPortal
        {...this.props}
      >
        {'<Speaker>'}
        Device:
        <select
          style={{ width: '100%' }}
          onChange={event => this.setDevice(event.target.value)}
          value={selectedDevice}
        >
          {options}
        </select>
        volume:
        <input
          type="range"
          min="0"
          max="1"
          step="0.1"
          value={volume}
          onChange={event => this.setVolume(event.target.value)}
        />
      </SettingPortal>
    );
  }
  render() {
    const {
      devices,
      selectedDevice,
      volume,
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
        {this.renderSetting()}
      </SingleBox>
    );
  }
}
