import React, {Component} from 'react';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';

import Question from '../components/question/Question';
import Profile from '../components/profile/Profile';
import Category from '../components/category';
import ScreamSkeleton from '../util/ScreamSkeleton';

import {connect} from 'react-redux';
import {
  getCategories,
  getQuestions,
  resetHome,
} from '../redux/actions/dataActions';
import InfiniteScroll from 'react-infinite-scroller';

let page = 1;

class home extends Component {
  state = {
    pageIndex: 1,
    firstLoad: false,
  };

  componentDidMount() {
    this.props.getQuestions();
    this.props.getCategories();
  }

  componentWillUnmount() {
    this.props.resetHome();
  }

  fetchMoreData = () => {
    const {loading} = this.props.data;
    if (loading) {
      return;
    }
    page++;
    this.props.getQuestions(page);
  };

  render() {
    const {firstLoad} = this.state;
    const {questions, categories, totalQuestion, loading} = this.props.data;

    const recentQuestionMarkup = firstLoad ? (
      <ScreamSkeleton />
    ) : (
      (questions || []).map(question => (
        <Question key={question.questionId} question={question} />
      ))
    );

    return (
      <Grid container spacing={16}>
        <Grid item sm={3} xs={12}>
          <Category categories={categories} />
          <Profile />
        </Grid>
        <Grid item sm={9} xs={12}>
          <InfiniteScroll
            pageStart={0}
            loadMore={this.fetchMoreData.bind(this)}
            hasMore={totalQuestion > questions.length}
            loader={
              <div className="loader" key={0}>
                Loading ...
              </div>
            }
          >
            {recentQuestionMarkup}
          </InfiniteScroll>
        </Grid>
      </Grid>
    );
  }
}

home.propTypes = {
  getQuestions: PropTypes.func.isRequired,
  getCategories: PropTypes.func.isRequired,
  resetHome: PropTypes.func.isRequired,
  data: PropTypes.object.isRequired,
};

const mapStateToProps = state => {
  console.log(state.data);
  return {
    data: state.data,
  };
};

export default connect(mapStateToProps, {
  getCategories,
  getQuestions,
  resetHome,
})(home);
