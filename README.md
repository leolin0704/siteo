<pre>const source = `import   { A,B,C } from '@commonComponent'

import {
    D,
    E,
    F
} from '@commonComponent'

import  { G } from '@commonComponent'
import\t{ G } from '@commonComponent'
import * as all from '@commonComponent'

`

const reg = new RegExp(/import\s+\{([^\}]*)\}\s+from\s+'@commonComponent'?/g)

let result
while(result = reg.exec(source)) {
    const modules = result[1].replace(/[\r\n\t\s]/g, '').split(',')
    console.log(modules)
}

let finalText = source.replace(reg, function(full, deps) {
    const modules = deps.replace(/[\r\n\t\s]/g, '').split(',')
    console.log(modules)
    return modules.reduce((prev, current, index) => {
        return [prev, `import { ${current} } from 'mymodules'`].join('\n')
    }, '')
})

console.log(finalText)</pre>
