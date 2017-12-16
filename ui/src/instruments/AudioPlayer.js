import React, { Component } from 'react';
import Instrument from './Instrument';
import SingleBox from './SingleBox';
import Jack from './Jack';

export default class AudioPlayer extends Instrument {
  play() {
    const { uuid } = this.props;
    fetch(`http://localhost:8080/audioplayer/${uuid}/play`, {
      method: 'POST',
    });
  }
  handleFileUpload(event) {
    const file = event.target.files[0];
    const { uuid } = this.props;

    const data = new FormData();
    data.append('file', file);
    fetch(`http://localhost:8080/audioplayer/${uuid}/upload`, {
      method: 'POST',
      body: data,
    });
  }
  render() {
    return (
      <SingleBox {...this.props}>
        AudioPlayer
        File:<input type="file" onChange={(e) => this.handleFileUpload(e)} />
        <button onClick={() => this.play()}>Play</button>
      </SingleBox>
    );
  }
}
