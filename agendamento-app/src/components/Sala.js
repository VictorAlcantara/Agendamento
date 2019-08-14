import React, { Component } from 'react';
import Table from 'react-bootstrap/Table';
import { Button, ButtonToolbar } from 'react-bootstrap';
import { AddEditSalaModal } from './AddEditSalaModal';

export class Sala extends Component {
    constructor(pros) {
        super(pros);
        this.state = { salas: [], addEditModalShow: false };

        this.refreshList = this.refreshList.bind(this);
        this.deleteSala = this.deleteSala.bind(this);
    }

    componentDidMount() {
        this.refreshList();
    }

    deleteSala(id) {
        fetch("http://localhost:4655/api/v1/sala/" + id, {
            method: "DELETE",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            },
        }).then(response => {
            this.refreshList();
        });
    }

    refreshList() {
        fetch("http://localhost:4655/api/v1/sala")
            .then(response => response.json())
            .then(data => {
                this.setState({ salas: data });
            });
    }

    render() {
        const { salas, id, descricao } = this.state;

        let addEditModalClose = () => this.setState({ addEditModalShow: false });

        return (
            <div>
                <Table className="mt-4" striped bordered hover variant="dark">
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Descrição</th>
                        </tr>
                    </thead>
                    <tbody>
                        {salas.map(sala =>
                            <tr key={sala.id}>
                                <td>{sala.id}</td>
                                <td>{sala.descricao}</td>
                                <td>
                                    <ButtonToolbar>
                                        <Button variant="info" size="sm"
                                            onClick={() => this.setState({ addEditModalShow: true, isAdd: false, id: sala.id, descricao: sala.descricao })}>
                                            Editar
                                        </Button>
                                        <Button variant="danger" size="sm" onClick={() => this.deleteSala(sala.id)}>
                                            Excluir
                                        </Button>
                                    </ButtonToolbar>
                                </td>
                            </tr>
                        )}
                    </tbody>
                </Table>
                <ButtonToolbar>
                    <Button variant="primary" onClick={() => this.setState({ addEditModalShow: true, isAdd: true })}>Adicionar Sala</Button>
                </ButtonToolbar>

                <AddEditSalaModal show={this.state.addEditModalShow} isAdd={this.state.isAdd} onHide={addEditModalClose}
                    id={id} descricao={descricao} onChangeHandler={this.refreshList} />
            </div>
        );
    }
}
