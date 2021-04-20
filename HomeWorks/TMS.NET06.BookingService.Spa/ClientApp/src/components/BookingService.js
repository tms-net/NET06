import React, { Component } from 'react';
import { Container, Row, Col, ListGroup, ListGroupItem } from 'reactstrap';

export class BookingService extends Component {

    constructor(props) {
        super(props);
        this.state = { services: [], loading: true };
    }

    selectService(service) {
        console.log(service.serviceId);
    }

    componentDidMount() {
        this.populateServices();
    }

    render() {
        if (this.state.loading){
            return <p><em>Loading...</em></p>
        }

        return (
            <Container>
                <Row>
                    <Col>
                        <h2>Welcome to Barber Shop!</h2>
                    </Col>
                </Row>
                <Row>
                    <Col>
                        <ListGroup>
                    {this.state.services.map(service =>
                        <ListGroupItem key={service.serviceId} tag="button" onClick={() => this.selectService(service)}>{service.name}</ListGroupItem>
                    )}                            
                       </ListGroup>
                    </Col>
                </Row>
            </Container>
        )
    }

    async populateServices() {        
        const response = await fetch('services');
        const data = await response.json();        
        this.setState({ services: data, loading: false });
    }
}