import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import { BrowserRouter, Route, Switch, Redirect } from "react-router-dom";

import AddTaskForm from './forms/AddTaskForm';
import ShortTaskTable from "./tables/ShortTaskTable";

ReactDOM.render(
  <BrowserRouter>
    <App>
      <Switch>
        <Route exact path="/">
          <Redirect to="/tasks" />
        </Route>
        <Route path='/tasks' component={ShortTaskTable}/>
        <Route path='/task/add' component={AddTaskForm} />
        <Route path='/task/:id(\d+)' />
      </Switch>
    </App>
  </BrowserRouter>,
  document.getElementById('root')
);