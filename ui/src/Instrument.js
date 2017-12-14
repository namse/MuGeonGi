import React, { Component } from 'react';

export default class Instrument extends Component {
  componentWillUnmount() {
    const { uuid } = this.props;
    fetch(`http://localhost:8080/instrument/${uuid}`, {
      method: 'delete',
    })
      .then(res => console.log(`delete instrument : ${res.status}`));
  }
}
