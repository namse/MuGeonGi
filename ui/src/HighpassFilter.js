import React from 'react';
import Instrument from './Instrument';
import Box from './Box';
import Jack from './Jack';

export default class HighpassFilter extends Instrument {
  render() {
    console.log(this.props);
    return (
      <Box>
        <Jack {...this.props.inputJack} />
        <Jack {...this.props.outputJack} />
      </Box>
    );
  }
}
