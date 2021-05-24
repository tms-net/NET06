import React, { Component } from 'react';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
      this.state = {
          productRows: [
              {
                  name: "",
                  calPerUnit: 10,
                  mass: 0,
                  calories: 0
              }
          ]
      };
    }

    handleMassChange(index, value) {
        var productRows = this.state.productRows;
        productRows[index].mass = value;
        productRows[index].calories = productRows[index].calPerUnit * productRows[index].mass;
        this.setState({ productRows: productRows });
    }

  componentDidMount() {    
  }

  renderForecastsTable() {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>#</th>
            <th>Name</th>
            <th>Mass</th>
            <th>Calories</th>
          </tr>
        </thead>
        <tbody>
          {this.state.productRows.map((row, index) =>
              <tr key={index}>
              <td>{index + 1}</td>
              <td><input value={row.name}/></td>
                  <td><input type="number" min="1" max="50" value={row.mass} onChange={(event) => this.handleMassChange(index, event.target.value)} /></td>
              <td>{row.calories}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
      return this.renderForecastsTable(this.state.forecasts);
  }

  async populateWeatherData() {
      const response = await fetch('weatherforecast');
    const data = await response.json();
    this.setState({ forecasts: data, loading: false });
  }
}
