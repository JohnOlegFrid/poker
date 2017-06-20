<?php
class Command
    {
        public $commandName;
        public $args;

        function Command($commandName, $args)
        {
            $this->commandName = $commandName;
            $this->args = $args;
        }
    }
?>