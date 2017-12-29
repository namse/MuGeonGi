import React from 'react';
import Instrument from './Instrument';
import SingleBox from './SingleBox';
import SettingPortal from './SettingPortal';
import KeyBindingHelper from '../utils/KeyBindingHelper';

export default class Switch extends Instrument {
  static StatesWillSave = [
    'isOpen',
  ];
  constructor(props) {
    super(props);
    this.state = {
      isOpen: false,
      accelerator: '...',
    };
    this.keyBindingHelper = new KeyBindingHelper(
      () => this.toggleCircuit(),
      accelerator => this.setState({ accelerator }),
    );
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
          {this.state.isOpen ? 'Open' : 'Close'}
        </button>

        <SettingPortal
          {...this.props}
        >
          {'<Switch>'}
          <button onClick={() => this.keyBindingHelper.bindKey()}>{this.state.accelerator}</button>
        </SettingPortal>
      </SingleBox>
    );
  }
}
