import React from 'react';
import Instrument from './Instrument';
import SingleBox from './SingleBox';
import SettingPortal from './SettingPortal';

export default class HighpassFilter extends Instrument {
  constructor(props) {
    super(props);
    this.state = {
      frequency: 1000,
    };
  }
  setFrequency(frequency) {
    this.setState({
      frequency,
    });
    const { uuid } = this.props;
    fetch(`http://localhost:8080/highpassfilter/${uuid}/SetFrequency/${frequency}`, {
      method: 'post',
    })
      .then(res => console.log(`set frequency of highpassfilter : ${res.status}`));
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
