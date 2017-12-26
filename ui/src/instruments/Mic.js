import React from 'react';
import Instrument from './Instrument';
import SingleBox from './SingleBox';
import SettingPortal from './SettingPortal';

export default class Mic extends Instrument {
  static StatesWillSave = [
    'device',
  ];
  constructor(props) {
    super(props);
    this.state = {
      devices: [],
      device: undefined,
    };
    const {
      uuid,
    } = props;
    console.log(props);
    fetch(`http://localhost:8080/mic/${uuid}/devices`)
      .then(res => res.json())
      .then(devices => this.setState({ devices: ['', ...devices] }))
      .catch(() => console.log('hi'));
  }
  componentWillUnmount() {
    const { uuid } = this.props;
    fetch(`http://localhost:8080/instrument/${uuid}`, {
      method: 'delete',
    })
      .then(res => console.log(`delete mic : ${res.status}`));
  }
  turnOn = () => {
    const { uuid } = this.props;
    fetch(`http://localhost:8080/mic/${uuid}/TurnOn`, {
      method: 'post',
    })
      .then(res => console.log(`turn on mic : ${res.status}`));
  }
  render() {
    const {
      devices,
      device,
    } = this.state;
    const options = devices.map(_device => <option value={_device}>{_device}</option>);
    return (
      <SingleBox
        {...this.props}
      >
        Mic
        Device:
        <select
          style={{ width: '100%' }}
          onChange={event => this.setDevice(event.target.value)}
          value={device}
        >
          {options}
        </select>
        <button onClick={() => this.turnOn()}>Turn On</button>
        <SettingPortal
          {...this.props}
        >
          Device:
          <select
            style={{ width: '100%' }}
            onChange={event => this.setDevice(event.target.value)}
            value={device}
          >
            {options}
          </select>
        </SettingPortal>
      </SingleBox>
    );
  }
}
