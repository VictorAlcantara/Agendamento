import React, { Component } from 'react';
import Jumbotron from 'react-bootstrap/Jumbotron';
import Container from 'react-bootstrap/Container';

export class Home extends Component {
    render() {
        return (
            <Jumbotron fluid>
                <Container>
                    <h5>Para iniciar...</h5>
                    <p>
                        Faça o cadastro das salas e marque os agendamentos.
                    </p>
                </Container>
            </Jumbotron>
        );
    }
}
