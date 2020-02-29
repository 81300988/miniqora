import React, {Component} from 'react';
import PropTypes from 'prop-types';
import QuestionCategoryPage from '../components/question/QuestionCategoryPage';
import Grid from '@material-ui/core/Grid';
import ScreamSkeleton from '../util/ScreamSkeleton';
import InfiniteScroll from 'react-infinite-scroller';

import {connect} from 'react-redux';
import {
  getQuestions,
  resetCategoryPage,
} from '../redux/actions/categoryActions';

let page = 1;
class Category extends Component {
  state = {
    categoryId: '',
  };

  componentDidMount() {
    const {categoryId} = this.props.match.params;

    this.setState({categoryId: categoryId});
    this.props.getQuestions({categoryId});
  }

  componentWillUnmount() {
    this.props.resetCategoryPage();
  }

  fetchMoreData = () => {
    const {loading} = this.props.category;
    const {categoryId} = this.state;

    if (loading) {
      return;
    }
    page++;
    this.props.getQuestions({categoryId, page});
  };

  render() {
    const {questions, totalQuestion, loading} = this.props.category;

    const recentQuestionMarkup =
      !questions.length && loading ? (
        <ScreamSkeleton />
      ) : (
        questions.map(question => (
          <QuestionCategoryPage key={question.questionId} question={question} />
        ))
      );

    return (
      <Grid container spacing={16}>
        <Grid item sm={8} xs={12}>
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

Category.propTypes = {
  getQuestions: PropTypes.func.isRequired,
  resetCategoryPage: PropTypes.func.isRequired,
  category: PropTypes.object.isRequired,
};

const mapStateToProps = state => {
  return {
    category: state.category,
  };
};

export default connect(mapStateToProps, {getQuestions, resetCategoryPage})(
  Category
);
