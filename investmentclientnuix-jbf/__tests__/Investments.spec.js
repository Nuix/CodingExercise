import {mount} from '@vue/test-utils';
import {describe, it, expect,} from 'vitest';
import {Investments} from '/src/components/Investments.vue';


//Very basic unit test just to ensure the component renders
describe('Investments.vue', () => {
    it('renders a table based on the userId'), () => {
        const wrapper = mount(Investments);
        expect(wrapper.text()).toContain("Investments:");
    }
});