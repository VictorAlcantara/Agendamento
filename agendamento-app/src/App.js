import React from 'react';
import './App.css';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import { Home } from './components/Home';
import { Sala } from './components/Sala';
import { Agenda } from './components/Agenda';
import { Navigation } from './components/Navigation';

function App() {
  return (
    <BrowserRouter>
      <div className="container">
        <Navigation />
        <Switch>
          <Route path="/" component={Home} exact />
          <Route path="/Home" component={Home} />
          <Route path="/Sala" component={Sala} />
          <Route path="/Agenda" component={Agenda} />
        </Switch>
      </div>
    </BrowserRouter>
  );
}

export default App;
