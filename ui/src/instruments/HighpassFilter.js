import React from 'react';
import Instrument from './Instrument';
import SingleBox from './SingleBox';
import Jack from './Jack';

export default class HighpassFilter extends Instrument {
  render() {
    console.log(this.props);
    return (
      <SingleBox {...this.props} />
    );
  }
}
