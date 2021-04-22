import React, { Component } from 'react';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css'

export class Calendar extends Component {
    constructor(props) {
        super(props);
        this.state = { availableDates: null};
        this.serviceId = this.props.serviceId;
    }

    componentDidMount() {
        this.populateAvailableDates();
    }

    render() {
        return (
            <div>
                <h1>Select Date</h1>
                {this.state.availableDates == null
                    ? <div>Our neural network is processing you data...</div>
                    : <DatePicker
                            includeDates={this.state.availableDates}
                            inline />
                }
            </div>
        )
    }

    async populateAvailableDates() {
        const response = await fetch('services/availableDates',{
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ serviceId: this.serviceId })
        });
        const data = await response.json();
        this.setState({ availableDates: data.map(dateString => new Date(dateString)) });
    }
}