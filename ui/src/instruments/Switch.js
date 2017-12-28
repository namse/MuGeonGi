import React from 'react';
import Instrument from './Instrument';
import SingleBox from './SingleBox';
import SettingPortal from './SettingPortal';

export default class Switch extends Instrument {
  static StatesWillSave = [
    'isOpen',
  ];
  constructor(props) {
    super(props);
    this.state = {
      isOpen: false,
    };
  }
  async toggleCircuit() {
    const { isOpen } = this.state;
    this.setState({
      isOpen: !isOpen,
    });
    await this.setIsOpen(!isOpen);
  }
  render() {
    return (
      <SingleBox {...this.props} >
        Switch
        <button onClick={() => this.toggleCircuit()}>
          {this.state.isOpen ? 'Close' : 'Open'}
        </button>

        <SettingPortal
          {...this.props}
        >
          {'<Highpass Filter>'}
          Frequency:
          <input
            value={this.state.frequency}
            onChange={event => this.setFrequency(event.target.value)}
          />
        </SettingPortal>
      </SingleBox>
    );
  }
}
