import React from 'react';
import withStyles from '@material-ui/core/styles/withStyles';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemAvatar from '@material-ui/core/ListItemAvatar';
import ListItemText from '@material-ui/core/ListItemText';
import {Link} from 'react-router-dom';
import Avatar from '@material-ui/core/Avatar';
import Typography from '@material-ui/core/Typography';

const styles = {
  root: {
    width: '100%',
    maxWidth: 360,
  },
};

const Category = ({classes, categories}) => {
  return (
    <div className={classes.root}>
      <List component="nav" aria-label="main mailbox folders">
        {categories.map(item => (
          <Typography
            key={item.categoryId}
            component={Link}
            to={`/category/${item.categoryId}`}
            color="primary"
          >
            <ListItem button>
              <ListItemAvatar>
                <Avatar
                  src={`${process.env.REACT_APP_API_URL}${item.imageName}`}
                />
              </ListItemAvatar>
              <ListItemText primary={item.categoryName} />
            </ListItem>
          </Typography>
        ))}
      </List>
    </div>
  );
};

export default withStyles(styles)(Category);
