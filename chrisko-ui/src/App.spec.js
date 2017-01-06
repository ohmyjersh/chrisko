import renderer from 'react-test-renderer';
import App, {inputBar} from './App';

test('Link renders correctly', () => {
  const tree = renderer.create(
    inputBar()
  ).toJSON();
  expect(tree).toMatchSnapshot();
});