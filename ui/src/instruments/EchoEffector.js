import React from 'react';
import Instrument from './Instrument';
import SingleBox from './SingleBox';
import SettingPortal from './SettingPortal';

export default class EchoEffector extends Instrument {
  static StatesWillSave = [
    'rightDelay',
    'wetDryMix',
    'feedback',
    'leftDelay',
    'panDelay',
    'rightDelayMin',
    'rightDelayMax',
    'wetDryMixMin',
    'wetDryMixMax',
    'feedbackMin',
    'feedbackMax',
    'leftDelayMin',
    'leftDelayMax',
    'panDelayMin',
    'panDelayMax',
  ];
  // 1. min max 값 가져와서 값 수정할 때 min, max 조절해주고
  // 2. setter 만들어서 react state 바꿔주고, 서버에다가도 바꿔주기
  render() {
    console.log(this.props);
    console.log(this.state);
    return (
      <SingleBox {...this.props} >
        Echo Effector

        <SettingPortal
          {...this.props}
        >
          {'<Echo Effector>'}
          <h3>rightDelay</h3>
          <input
            type="range"
            value={this.state.rightDelay}
            min={this.state.rightDelayMin}
            max={this.state.rightDelayMax}
            onChange={(event) => { this.rightDelay = event.target.value; }}
          />
          <span>{this.state.rightDelay}</span>
          <h3>wetDryMix</h3>
          <input
            type="range"
            value={this.state.wetDryMix}
            min={this.state.wetDryMixMin}
            max={this.state.wetDryMixMax}
            onChange={(event) => { this.wetDryMix = event.target.value; }}
          />
          <span>{this.state.wetDryMix}</span>
          <h3>feedback</h3>
          <input
            type="range"
            value={this.state.feedback}
            min={this.state.feedbackMin}
            max={this.state.feedbackMax}
            onChange={(event) => { this.feedback = event.target.value; }}
          />
          <span>{this.state.feedback}</span>
          <h3>leftDelay</h3>
          <input
            type="range"
            value={this.state.leftDelay}
            min={this.state.leftDelayMin}
            max={this.state.leftDelayMax}
            onChange={(event) => { this.leftDelay = event.target.value; }}
          />
          <span>{this.state.leftDelay}</span>
          <h3>panDelay</h3>
          <input
            type="range"
            value={this.state.panDelay}
            min={this.state.panDelayMin}
            max={this.state.panDelayMax}
            onChange={(event) => { this.panDelay = event.target.value; }}
          />
          <span>{this.state.panDelay}</span>
        </SettingPortal>
      </SingleBox>
    );
  }
}
