import React from 'react';
import styled from 'styled-components';
import Instrument from './Instrument';
import SingleBox from './SingleBox';
import KeyBindingHelper from '../utils/KeyBindingHelper';

const FileInputButton = styled.button`
  width: 70px;
  margin: 0px;
  padding: 0px;
  overflow: hidden;
  text-overflow: ellipsis;
`;

export default class AudioPlayer extends Instrument {
  constructor(props) {
    super(props);
    this.state = {
      filename: 'dont_click',
      accelerator: '...',
    };
    this.keyBindingHelper = new KeyBindingHelper(
      () => this.play(),
      accelerator => this.setState({ accelerator }),
    );
  }
  play() {
    const { uuid } = this.props;
    fetch(`http://localhost:8080/audioplayer/${uuid}/play`, {
      method: 'POST',
    });
  }
  handleFileUpload(event) {
    const file = event.target.files[0];
    const { uuid } = this.props;
    this.setState({
      filename: '...',
    });
    const data = new FormData();
    data.append('file', file);
    fetch(`http://localhost:8080/audioplayer/${uuid}/upload`, {
      method: 'POST',
      body: data,
    })
      .then(() => {
        this.setState({
          filename: file.name,
        });
      });
  }
  render() {
    return (
      <SingleBox {...this.props}>
        AudioPlayer

        File:
        <FileInputButton
          onClick={() => { this.fileInput.click(); }}
        >{this.state.filename}
        </FileInputButton>
        <input
          style={{ display: 'none' }}
          ref={(fileInput) => { this.fileInput = fileInput; }}
          type="file"
          onChange={event => this.handleFileUpload(event)}
        />
        <button onClick={() => this.keyBindingHelper.bindKey()}>{this.state.accelerator}</button>
        <button onClick={() => this.play()}>Play</button>
      </SingleBox>
    );
  }
}
