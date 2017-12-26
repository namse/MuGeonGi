import React from 'react';
import Instrument from './Instrument';
import SingleBox from './SingleBox';
import SettingPortal from './SettingPortal';

export default class HighpassFilter extends Instrument {
  static StatesWillSave = [
    'frequency',
  ];
  constructor(props) {
    super(props);
    this.state = {
      frequency: 1000,
    };
  }
  render() {
    console.log(this.props);
    return (
      <SingleBox {...this.props} >
        Highpass Filter
        Frequency: {this.state.frequency}Hz

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
