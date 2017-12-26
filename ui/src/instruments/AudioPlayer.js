import React from 'react';
import styled from 'styled-components';
import Instrument from './Instrument';
import SingleBox from './SingleBox';
import hookKeboard, { convertToAccelerator } from '../utils/hookKeboard';

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
    this.playKeyHookSymbol = Symbol(props.uuid);
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
  bindKey() {
    const SHIFT = 16;
    const CTRL = 17;
    const ALT = 18;
    const combinationKeys = [SHIFT, CTRL, ALT];
    const { uuid } = this.props;
    const handler = (event) => {
      event.preventDefault();
      if (combinationKeys.includes(event.which)) {
        return;
      }
      hookKeboard(this.playKeyHookSymbol, event, () => {
        this.play();
      });
      const accelerator = convertToAccelerator(event);
      this.setState({
        accelerator,
      });
      document.removeEventListener('keydown', handler);
    };
    document.addEventListener('keydown', handler);
  }
  render() {
    const {
      filename,
      accelerator,
    } = this.state;
    return (
      <SingleBox {...this.props}>
        AudioPlayer

        File:
        <FileInputButton
          onClick={() => { this.fileInput.click(); }}
        >{filename}
        </FileInputButton>
        <input
          style={{ display: 'none' }}
          ref={(fileInput) => { this.fileInput = fileInput; }}
          type="file"
          onChange={event => this.handleFileUpload(event)}
        />
        Key:<button onClick={() => this.bindKey()}>{accelerator}</button>
        <button onClick={() => this.play()}>Play</button>
      </SingleBox>
    );
  }
}
